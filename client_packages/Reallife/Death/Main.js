let deathBrowser = null;

mp.events.add('DeathTrue', () => {
	
	deathBrowser = mp.browsers.new('package://Reallife/Death/index.html');
	mp.gui.chat.activate(false);
	
});

mp.events.add('DeathFalse', () => {
	
	deathBrowser.destroy();
	mp.gui.chat.activate(true);
	
});