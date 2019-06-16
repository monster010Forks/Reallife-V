
const rentPos= [new mp.Vector3(-1153.755, -718.8862, 20.97753),];

function atRent (player) {
	let closestRent = 0;
    let minDist = 2.0;
	
	for (let i = 0, max = rentPos.length; i < max; i++) {
        let dist = player.dist(rentPos[i]);
        if (dist < minDist) {
            minDist = dist;
            closestRent = i;
			player.call("HotkeyRent", [player]);
        }
    }
}

const bankPincPos = [new mp.Vector3(243.1547, 224.7177, 106.2868), new mp.Vector3(-1214.762, -331.7896, 37.7907), new mp.Vector3(-352.7701, -49.25988, 49.04625), 
					 new mp.Vector3(312.1963, -278.3508, 54.17446), new mp.Vector3(148.3513, -1040.156, 29.37776), new mp.Vector3(-2962.656, 480.7722, 15.70677),
					 new mp.Vector3(1176.94, 2706.805, 38.09771), ];
					
function atBankPinc (player) {
	let closestPinc = 0;
    let minDist = 0.5;
	
	for (let i = 0, max = bankPincPos.length; i < max; i++) {
        let dist = player.dist(bankPincPos[i]);
        if (dist < minDist) {
            minDist = dist;
            closestPinc = i;
			player.call("HotkeyBankPinc", [player]);
        }
    }
}

const bankErstPos = [new mp.Vector3(243.1547, 224.7177, 106.2868),new mp.Vector3(-1212.084, -330.4282, 37.78704),new mp.Vector3(-350.551, -50.09611, 49.04259),
					 new mp.Vector3(314.8576, -279.327, 54.17081),new mp.Vector3(150.402, -1040.878, 29.3741),new mp.Vector3(-2962.552, 483.3518, 15.70312),
					 new mp.Vector3(1174.971, 2706.805, 38.09408),]

function atBankErst (player) {
	let closestBankEst = 0;
    let minDist = 0.5;
	
	for (let i = 0, max = bankErstPos.length; i < max; i++) {
        let dist = player.dist(bankErstPos[i]);
        if (dist < minDist) {
            minDist = dist;
            closestBankEst = i;
			player.call("HotkeyBankErst", [player]);
        }
    }
}

//BANK POS
const bankPos = [
	new mp.Vector3(155,6642,31), new mp.Vector3(132,6366,31), new mp.Vector3(-282,6225,31), new mp.Vector3(-386,6045,31), new mp.Vector3(1701,6426,32), //
	new mp.Vector3(1735,6410,35), new mp.Vector3(1703,4933,42), new mp.Vector3(1686,4816,42), new mp.Vector3(1822,3683,34), new mp.Vector3(1968,3743,32), //
	new mp.Vector3(-258,-723,33), new mp.Vector3(-256,-715,33), new mp.Vector3(-254,692,33), new mp.Vector3(-28,-723,44), new mp.Vector3(1078,-776,57), //
	new mp.Vector3(1138,-468,66), new mp.Vector3(1166,-456,66), new mp.Vector3(1153,-326,69), new mp.Vector3(285,143,104), new mp.Vector3(89,2,67), //
	new mp.Vector3(-56,-1752,29), new mp.Vector3(33,-1348,29), new mp.Vector3(288,-1282,29), new mp.Vector3(289,-1256,29), new mp.Vector3(146,-1035,29), //
	new mp.Vector3(199,-883,31), new mp.Vector3(112,-775,31), new mp.Vector3(112,-819,31), new mp.Vector3(296,-895,29), new mp.Vector3(-1827,784,138), //
	new mp.Vector3(-1410,-99,52), new mp.Vector3(1570,-546,34), new mp.Vector3(2683,3286,55), new mp.Vector3(2564,2584,38), new mp.Vector3(1171,2702,38), //
	new mp.Vector3(-1091,2708,18), new mp.Vector3(-1827,784,138), new mp.Vector3(-1410,-99,52), new mp.Vector3(-1540,-546,34), new mp.Vector3(-254,-692,33), //
	new mp.Vector3(-302,-829,32), new mp.Vector3(-526,-1222,18), new mp.Vector3(-537,-854,29), new mp.Vector3(-613,-704,31), new mp.Vector3(-660,-854,24),
	new mp.Vector3(-711,-818,23), new mp.Vector3(-717,-915,19),
];
//BANK POS ENDE

function atATM (player) {
	let closestAtm = 0;
    let minDist = 3.0;
	
	for (let i = 0, max = bankPos.length; i < max; i++) {
        let dist = player.dist(bankPos[i]);
        if (dist < minDist) {
            minDist = dist;
            closestAtm = i;
			player.call("OpenATM", [player]); 
        }
    }
}

//MOSLEY AUTOHAUS
const mosPOs= [new mp.Vector3(-40.82757, -1674.866, 29.5),];

function atMos (player) {
	let closestMos = 0;
    let minDist = 4.0;
	
	for (let i = 0, max = mosPOs.length; i < max; i++) {
        let dist = player.dist(mosPOs[i]);
        if (dist < minDist) {
            minDist = dist;
            closestMos = i;
			player.call("OpenMos", [player]);
        }
    }
}

mp.events.add("keypress:E", (player) => {
	if(atRent(player)){}
	
	if(atBankPinc(player)) {}
	
	if(atBankErst(player)) {}
	
	if(atATM(player)) {}
	
	if(atMos(player)) {}
});


	

