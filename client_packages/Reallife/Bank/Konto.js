let bKontoBrowser = null;

mp.events.add("bKontoToServer", (pin, repin) => {
	mp.events.callRemote("OnPlayerbKonto", pin, repin);
});

//BANKKONTO ERSTELLUNG
mp.events.add("StartbKontoBrowser", () => {
	bKontoBrowser = mp.browsers.new("package://Reallife/Bank/bKonto.html");
	
	mp.gui.cursor.show(true, true);
	mp.gui.cursor.visible = true;
    //mp.gui.chat.activate(false);
});

mp.events.add("bKontoResult", (result) => {
	if(result == 1) {
		bKontoBrowser.destroy();
		mp.gui.cursor.show(false, false);
		mp.gui.chat.activate(true);
	}
	else {
		bKontoBrowser.reload(true);
		mp.gui.cursor.show(true, true);
		mp.gui.chat.activate(true);
	}
});	

mp.events.add("ClosePinAuswahl", () => {
	mp.gui.cursor.visible = false;
	mp.gui.chat.activate(true);
	bKontoBrowser.destroy();
	mp.gui.chat.push("Bankkonto Erstellung Abbruch!");
});
