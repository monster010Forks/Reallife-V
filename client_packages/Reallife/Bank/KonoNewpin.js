let bPinChangeBrowser = null;

mp.events.add("bKontoNewpinToServer", (opin, pin, repin) => {
	mp.events.callRemote("OnPlayerPinChange", opin, pin, repin);
});

//BANKKONTO ERSTELLUNG
mp.events.add("StartPinChangeBrowser", () => {
	bPinChangeBrowser = mp.browsers.new("package://Reallife/Bank/bKontoNewpin.html");
	
	mp.gui.cursor.show(true, true);
	mp.gui.cursor.visible = true;
    //mp.gui.chat.activate(false);
});

mp.events.add("bChangepinResult", (result) => {
	if(result == 1) {
		bPinChangeBrowser.destroy();
		mp.gui.cursor.show(false, false);
		mp.gui.chat.activate(true);
	}
	else {
		bPinChangeBrowser.reload(true);
		mp.gui.cursor.show(true, true);
		mp.gui.chat.activate(true);
	}
});	

mp.events.add("ClosePinChaneAuswahl", () => {
	mp.gui.cursor.visible = false;
	mp.gui.chat.activate(true);
	bPinChangeBrowser.destroy();
	mp.gui.chat.push("Bankkonto Erstellung Abbruch!");
});
