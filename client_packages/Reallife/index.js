let camera = undefined;

mp.events.add("Camera", () => {	
	mp.game.ui.displayRadar(false);
	mp.game.ui.displayHud(false);
	mp.gui.chat.activate(false);
	
	camera = mp.cameras.new('default', new mp.Vector3(-370.5946,4403.613, 49.35886), new mp.Vector3(0,0,0), 40);

	camera.pointAtCoord(-481.8217,4408.182,64.39824); //Changes the rotation of the camera to point towards a location
	camera.setActive(true);
	mp.game.cam.renderScriptCams(true, false, 0, true, false);
});
mp.events.add("CameraDestroy", () => {
	mp.game.ui.displayRadar(true);
	mp.game.ui.displayHud(true);
	mp.gui.chat.activate(true);
	mp.gui.chat.show(true);
	
	mp.game.cam.renderScriptCams(false, false, 0, true, false);
	
	camera.setActive(false);
	camera.destroy();
	camera = undefined;
});

mp.events.add("ConnectFreeze", () => {
	mp.players.local.freezePosition(true);
});

mp.events.add("LoginUnFreeze", () => {
	mp.players.local.freezePosition(false);
});

//GARAGE FLUGHAFEN
let Ped3 = mp.peds.new(mp.game.joaat('s_m_m_autoshop_01'), new mp.Vector3( -876.334, -2741.49, 13.95726), 56.40747, (streamPed) => {
    // Ped Streamed
    streamPed.setAlpha(0);
}, 0);

//MOSLEY AUTOHAUS
let Ped2 = mp.peds.new(mp.game.joaat('s_m_m_autoshop_01'), new mp.Vector3( -40.57611, -1674.582, 29.48332), 142.1193, (streamPed) => {
    // Ped Streamed
    streamPed.setAlpha(0);
}, 0);

//RENT CAR 127.3255
let Ped = mp.peds.new(mp.game.joaat('a_m_y_bevhills_01'), new mp.Vector3( -1153.755, -718.8862, 20.97753), 127.3255, (streamPed) => {
    // Ped Streamed
    streamPed.setAlpha(0);
}, 0);

//HOTKEY E
mp.events.add('HotkeyRent', (player) => {
    mp.events.callRemote('HotRent', player);
});

mp.events.add("HotkeyBankPinc", (player) => {
	mp.events.callRemote("bKontob", player);
});

mp.events.add("HotkeyBankErst", (player) => {
	mp.events.callRemote("bKontoErst", player);
});

mp.events.add("OpenATM", (player) => {
	mp.events.callRemote("OpenATMcode", player);
});
mp.events.add("OpenMos", (player) => {
	mp.events.callRemote("OpenMosMenu", player);
});
mp.keys.bind(0x45, false, function() {
    mp.events.callRemote('keypress:E');
});






