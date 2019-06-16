let autoBrowser = null;

mp.events.add("StartAutohausBrowser", () => {
	autoBrowser = mp.browsers.new('package://Reallife/Autohaus/Mosley/index.html');
	mp.gui.cursor.show(true, true);		
});
	
mp.events.add('BuyVehilceToClient1', () => {
    mp.events.callRemote('OnPlayerBuyVehicle1');
	autoBrowser.destroy();
    mp.gui.cursor.show(false, false);
});

mp.events.add('BuyVehilceToClient2', () => {
    mp.events.callRemote('OnPlayerBuyVehicle2');
	autoBrowser.destroy();
    mp.gui.cursor.show(false, false);
});


mp.events.add("BuyVehicleClose", () => {
	autoBrowser.destroy();
    mp.gui.cursor.show(false, false);
});