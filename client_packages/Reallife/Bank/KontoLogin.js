let bKontoLoginBrowser = null;

mp.events.add("bKontoLoginToServer", (pin) => {
	mp.events.callRemote("OnplayerbKontoLogin", pin);
});

//BANKKONTO
mp.events.add("StartbKontoLoginBrowser", () => {
	bKontoLoginBrowser = mp.browsers.new("package://Reallife/Bank/bKontoLogin.html");
	
	mp.gui.cursor.show(true, true);
	mp.gui.cursor.visible = true;
    //mp.gui.chat.activate(false);
});

mp.events.add("bKontoLoginResult", (result) => {
	if(result == 1) {
		bKontoLoginBrowser.destroy();
		mp.gui.cursor.show(true, true);
		mp.gui.chat.activate(false);
	}
	else {
		bKontoLoginBrowser.reload(true);
		mp.gui.cursor.show(true, true);
		mp.gui.chat.activate(true);
	}
});	

mp.events.add("CloseMenu", () => {
	mp.gui.cursor.visible = false;
	mp.gui.chat.activate(true);
	bKontoLoginBrowser.destroy();
	mp.gui.chat.push("Bankkonto Login Abbruch!");
});
