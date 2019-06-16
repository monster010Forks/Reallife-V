$('#ErstellungButton').click(() => {

    $('.alert').remove();
    mp.trigger('bKontoToServer', $('#BankPin').val(), $('#BinPinRe').val());
});

$('#ClosePinAuswahlButton').click(() => {

    $('.alert').remove();
    mp.trigger('ClosePinAuswahl');
});
