$('#loginButton').click(() => {

    $('.alert').remove();
    mp.trigger('loginInformationToServer', $('#loginUsernameText').val(), $('#loginPasswordText').val());
});

$('#registerButton').click(() => {

    $('.alert').remove();
    mp.trigger('registerInformationToServer', $('#registerUsernameText').val(), $('#registerPasswordText').val(), $('#registerPasswordTextRe').val());
});