$(function () {
    var l = abp.localization.getResource('Harbour');
    var createModal = new abp.ModalManager(abp.appPath + 'Ships/CreateModal');
    var editModal = new abp.ModalManager(abp.appPath + 'Ships/EditModal');

    var dataTable = $('#ShipsTable').DataTable(
        abp.libs.datatables.normalizeConfiguration({
            serverSide: true,
            paging: true,
            order: [[1, "asc"]],
            searching: false,
            scrollX: true,
            ajax: abp.libs.datatables.createAjax(harbour.ships.ship.getList),
            columnDefs: [
                {
                    title: l('Actions'),
                    rowAction: {
                        items:
                            [
                                {
                                    text: l('Edit'),
                                    visible:
                                        abp.auth.isGranted('Harbour.Ships.Edit'),
                                    action: function (data) {
                                        editModal.open({ id: data.record.id });
                                    }
                                },
                                {
                                    text: l('Delete'),
                                    visible:
                                        abp.auth.isGranted('Harbour.Ships.Delete'),
                                    confirmMessage: function (data) {
                                        return l(
                                            'ShipDeletionConfirmationMessage',
                                            data.record.name
                                        );
                                    },
                                    action: function (data) {
                                        harbour.ships.ship
                                            .delete(data.record.id)
                                            .then(function () {
                                                abp.notify.info(
                                                    l('SuccessfullyDeleted')
                                                );
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
                    title: l('Type'),
                    data: "type"
                },
                {
                    title: l('YearBuilt '),
                    data: "yearBuilt"
                },
                {
                    title: l('PassengerCapacity '),
                    data: "passengerCapacity"
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

    $('#NewShipButton').click(function (e) {
        e.preventDefault();
        createModal.open();
    });
});
