var loginBrowser = mp.browsers.new('package://Reallife/Login/Login.html');
mp.gui.cursor.show(true, true);

mp.events.add('loginInformationToServer', (username, password) => {
    mp.events.callRemote('OnPlayerLoginAttempt', username, password);
});

mp.events.add('registerInformationToServer', (username, password, passwordre) => {
    mp.events.callRemote('OnPlayerRegisterAttempt', username, password,passwordre);
});


mp.events.add('RegisterResult', (result) => {
	if (result == 3) {
		
		//loginBrowser.destroy();
		//mp.gui.cursor.show(false, false);
		
		//require("./Reallife/Character/Main.js");
		//mp.events.callRemote("testnach");
		
		//loginBrowser.reload(true);

		loginBrowser.execute('document.getElementById("p2").innerHTML = "Du kannst dich nun einloggen!";');

	}
	else if(result = 2) {
		loginBrowser.execute('document.getElementById("p2").innerHTML = "Passwort stimmt nicht uber ein!";');
	}
	else if(result = 1){
		
		//loginBrowser.reload(true);
		
		loginBrowser.execute('document.getElementById("p2").innerHTML = "Du kannst dich nun einloggen!";');

			
	} else if(result = 0) {
		loginBrowser.execute('document.getElementById("p2").innerHTML = "Der Benutzername ist schon Vorhanden!";');
	}
});
mp.events.add('LoginResult', (result) => {
    if (result == 1) {

        loginBrowser.destroy();
        mp.gui.cursor.show(false, false);

     //   mp.gui.chat.push("Du hast dich Erfolgreich eingeloggt!");
    }
    else {

       // mp.gui.chat.push('Incorrect password or username.');

	// window.alert("Die Angebenen Daten sind nicht korrekt!");

        loginBrowser.execute('document.getElementById("p1").innerHTML = "Die Daten sind nicht korrekt!";');
    }
});