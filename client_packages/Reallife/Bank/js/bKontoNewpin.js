$('#ChangeButton').click(() => {

    $('.alert').remove();
    mp.trigger('bKontoNewpinToServer', $('#OldPin').val(), $('#NewPin1').val(), $('#NewPin2').val());
});

$('#ClosePinAuswahlChangeButton').click(() => {

    $('.alert').remove();
    mp.trigger('ClosePinChaneAuswahl');
});
