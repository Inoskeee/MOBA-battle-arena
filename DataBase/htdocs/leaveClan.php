<?php
	$connect = mysqli_connect("localhost", "root", "root", "unityaccess");

	if(mysqli_connect_errno())
	{
		echo "1"; //: Connection failed
		exit();
	}

	$username = $_POST["username"];
	$clanName = $_POST["clanName"];

	$clan_id = mysqli_query($connect, "SELECT clan_id, clan_raiting FROM clans WHERE clan_name = '".$clanName."';") or die("2");
	$existclanid = mysqli_fetch_assoc($clan_id);

	$player_id = mysqli_query($connect, "SELECT id FROM players WHERE username = '".$username."';") or die("2");
	$existplayerid = mysqli_fetch_assoc($player_id);



	$leaveclanquery = "DELETE FROM player_clans WHERE player_clans.player_id = ".$existplayerid["id"].";";

	mysqli_query($connect, $leaveclanquery) or die("5"); //: Insert player failed

	$namecheckquery = "SELECT * FROM characters WHERE characters.character_id = ".$existplayerid["id"].";";

	$namecheck = mysqli_query($connect, $namecheckquery) or die("2"); //: Name check query failed
	$existplayerid = mysqli_fetch_assoc($namecheck);

	$updateclanquery = "UPDATE clans SET clan_raiting = ".($existclanid["clan_raiting"]-$existplayerid["score"])." WHERE clans.clan_id = ".$existclanid["clan_id"].";";
	mysqli_query($connect, $updateclanquery) or die("5"); //: Insert player failed

	echo "0"; //: No errors
?>