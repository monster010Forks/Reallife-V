

$('#EinzahlungButton').click(() => {

    $('.alert').remove();
    mp.trigger('BankeinzahlungToServer', $('#SummeEinzahlen').val());
});

$('#AuszahlungButton').click(() => {

    $('.alert').remove();
    mp.trigger('BankauszahlungToServer', $('#SummeAuszahlen').val());
});

$('#bank-balance').click(() => {

    $('.alert').remove();
    mp.trigger('testanzeige');
});

$('#cancelBank').click(() => {

    $('.alert').remove();
    mp.trigger('CloseStat');
});

$('#UberweisungButton').click(() => {

    $('.alert').remove();
    mp.trigger('UberweisungToServer', $('#name').val(), $('#SummeUberweisen').val());
});

$('#Form').validator()

function showBankOperations(bankOperationsJson){
}