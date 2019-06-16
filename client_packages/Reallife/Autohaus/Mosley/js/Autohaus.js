$('#ConfirmAuto1').click(() => {

    $('.alert').remove();
    mp.trigger('BuyVehilceToClient1');
});

$('#ConfirmAuto2').click(() => {

    $('.alert').remove();
    mp.trigger('BuyVehilceToClient2');
});

$('#CloseButton').click(() => {

    $('.alert').remove();
    mp.trigger('BuyVehicleClose');
});