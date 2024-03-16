$(function () {
    var l = abp.localization.getResource('Harbour');
    var createModal = new abp.ModalManager(abp.appPath + 'Fleets/CreateModal');
    var editModal = new abp.ModalManager(abp.appPath + 'Fleets/EditModal');

    var dataTable = $('#FleetsTable').DataTable(
        abp.libs.datatables.normalizeConfiguration({
            serverSide: true,
            paging: true,
            order: [[1, "asc"]],
            searching: false,
            scrollX: true,
            ajax: abp.libs.datatables.createAjax(harbour.fleets.fleet.getList),
            columnDefs: [
                {
                    title: l('Actions'),
                    rowAction: {
                        items:
                            [
                                {
                                    text: l('Edit'),
                                     visible: abp.auth.isGranted('Harbour.Fleets.Edit'),
                                    action: function (data) {
                                        editModal.open({ id: data.record.id });
                                    }
                                },
                                {
                                    text: l('Delete'),
                                    visible: abp.auth.isGranted('Harbour.Fleets.Delete'),

                                    confirmMessage: function (data) {
                                        return l('FleetDeletionConfirmationMessage', data.record.name);
                                    },
                                    action: function (data) {
                                        harbour.fleets.fleet
                                            .delete(data.record.id)
                                            .then(function () {
                                                abp.notify.info(l('SuccessfullyDeleted'));
                                                dataTable.ajax.reload();
                                            });
                                    }
                                }

                            ]
                    }
                },
                {
                    title: l('Name'),
                    data: "name"
                },
                {
                    title: l('Ship'),
                    data: "shipName"
                },
                {
                    title: l('HarbourShip'),
                    data: "harbourShip",
                    render: function (data) {
                        return l('Enum:HarbourShips.' + data);
                    }
                },

               
                {
                    title: l('CreationTime'), data: "creationTime",
                    dataFormat: "datetime"
                }
            ]
        })
    );

    createModal.onResult(function () {
        dataTable.ajax.reload();
    });

    editModal.onResult(function () {
        dataTable.ajax.reload();
    });

    $('#NewFleetButton').click(function (e) {
        e.preventDefault();
        createModal.open();
    });
});
