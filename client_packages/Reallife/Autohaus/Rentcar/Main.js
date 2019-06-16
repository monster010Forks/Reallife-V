let rentBrowser = null;

mp.events.add("OpenRentBrowser", () => {
	rentBrowser = mp.browsers.new('package://Reallife/Autohaus/Rentcar/index.html');
	mp.gui.cursor.show(true, true);		
});
	
mp.events.add('RentRollerToClient1', () => {
    mp.events.callRemote('RentSpawnCarRoller1');
	rentBrowser.destroy();
    mp.gui.cursor.show(false, false);
});



mp.events.add("RentVehicleClose", () => {
	rentBrowser.destroy();
    mp.gui.cursor.show(false, false);
});