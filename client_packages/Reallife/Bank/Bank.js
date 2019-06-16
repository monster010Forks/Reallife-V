let bankBrowser = null;

mp.events.add("BankeinzahlungToServer", (summe) => {
	mp.events.callRemote("OnPlayerEinzahlung", summe);
});

mp.events.add("BankauszahlungToServer", (summe) => {
	mp.events.callRemote("OnPlayerAuszahlung", summe);
});

mp.events.add("UberweisungToServer", (name, summe) => {
	mp.events.callRemote("OnPlayerUberweisungAttempt", name, summe);
});

mp.events.add("StartBankBrowser", (handgeld) => {
	bankBrowser = mp.browsers.new("package://Reallife/Bank/Bank.html");
	
	mp.gui.cursor.show(true, true);
	mp.gui.cursor.visible = true;
    //mp.gui.chat.activate(false);
	
	
	bankBrowser.execute(`document.getElementById('handgeld').innerHTML = '${handgeld}';`);
	
});

mp.events.add("BankResult", (result) => {
	if(result == 1) {
		bankBrowser.destroy();
		mp.gui.cursor.show(false, false);
		mp.gui.chat.activate(true);
	}
	else {
		bankBrowser.destroy();
		mp.gui.cursor.show(false, false);
		mp.gui.chat.activate(true);
	}
});	

mp.events.add("CloseStat", () => {
	mp.gui.cursor.visible = false;
	mp.gui.chat.activate(true);
	bankBrowser.destroy();
	mp.gui.chat.push("Bank Menu Abbrechen!");
});
