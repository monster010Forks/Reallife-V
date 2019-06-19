mp.events.add('JailTrue', () => {
	mp.players.local.freezePosition(true);
});

mp.events.add('JailFalse', () => {
	mp.players.local.freezePosition(false);
});