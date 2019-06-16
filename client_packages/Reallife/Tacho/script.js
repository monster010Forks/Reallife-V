let speedo = mp.browsers.new("package://Reallife/Tacho/index.html");
let showed = false;
let player = mp.players.local;

mp.events.add('render', () =>
{
	if (player.vehicle && player.vehicle.getPedInSeat(-1) === player.handle)
	{
		if(showed === false)
		{
			speedo.execute("showSpeedo();");
			showed = true;
		}

        let vel1 = player.vehicle.getSpeed() * 3.6;
        let vel = (vel1).toFixed(0);
		
		speedo.execute(`update(${vel});`);
	}
	else
	{
		if(showed)
		{
			speedo.execute("hideSpeedo();");
			showed = false;
		}
	}
});