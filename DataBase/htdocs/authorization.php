<?php

	$connect = mysqli_connect("localhost", "root", "root", "unityaccess");

	if(mysqli_connect_errno())
	{
		echo "1"; //: Connection failed
		exit();
	}

	$username = $_POST["name"];
	$password = $_POST["password"];



	$namecheckquery = "SELECT id, username, hash, salt, email, players.character_id, score, head, body, legs, sword, bow FROM players INNER JOIN characters ON players.character_id = characters.character_id WHERE username = '".$username."';";

	$namecheck = mysqli_query($connect, $namecheckquery) or die("2"); //: Name check query failed

	if(mysqli_num_rows($namecheck) != 1)
	{
		echo "6"; //: No one users with this name
		exit();
	}

	$existinginfo = mysqli_fetch_assoc($namecheck);
	$salt = $existinginfo["salt"];
	$hash = $existinginfo["hash"];

	$loginhash = crypt($password, $salt);

	if($hash != $loginhash)
	{
		echo "7"; //: Incorrect password
		exit();
	}

	$clancheckquery = "SELECT username, clan_name, clan_leader FROM clans INNER JOIN player_clans ON clans.clan_id = player_clans.clan_id INNER JOIN players ON players.id = player_clans.player_id WHERE username = '".$username."';";

	$clancheck = mysqli_query($connect, $clancheckquery);

	$clanname = "";
	if(mysqli_num_rows($clancheck) == 1)
	{
		$selectedclan = mysqli_fetch_assoc($clancheck);

		$nameleaderquery = "SELECT * FROM players WHERE id = ".$selectedclan["clan_leader"].";";
		$leadercheck = mysqli_query($connect, $nameleaderquery) or die("2"); //: Name check query failed

		$existingleader = mysqli_fetch_assoc($leadercheck);

		$clanname = $selectedclan["clan_name"]."\t".$existingleader["username"];
	}
	else
	{
		$clanname = "\t";
	}
	//Boosters
	$booster = 0;

	$boosterquery = "SELECT * FROM boosters INNER JOIN character_booster ON character_booster.booster_id=boosters.booster_id INNER JOIN players ON players.character_id = character_booster.character_id WHERE players.username = '".$username."';";

	$boostercheck = mysqli_query($connect, $boosterquery);


	if(mysqli_num_rows($boostercheck) >= 1)
	{
		$selectedboost = mysqli_fetch_assoc($boostercheck);
		$booster = $selectedboost["booster_id"]."\t".$selectedboost["booster_features"]."\t";
		$boosterquery = "SELECT COUNT(*) FROM character_booster WHERE character_booster.character_id = ".$existinginfo["character_id"].";";
		$boostercheck = mysqli_query($connect, $boosterquery);
		$selectedboost = mysqli_fetch_assoc($boostercheck);
		$booster .= $selectedboost["COUNT(*)"];
	}

	if($booster == 0)
	{
		echo "0\t".$existinginfo["email"]."\t".$existinginfo["score"]."\t".$existinginfo["head"]."\t".$existinginfo["body"]."\t".$existinginfo["legs"]."\t".$existinginfo["sword"]."\t".$existinginfo["bow"]."\t".$clanname."\t0";
	}
	else
	{
		echo "0\t".$existinginfo["email"]."\t".$existinginfo["score"]."\t".$existinginfo["head"]."\t".$existinginfo["body"]."\t".$existinginfo["legs"]."\t".$existinginfo["sword"]."\t".$existinginfo["bow"]."\t".$clanname."\t".$booster;
	}




	#SELECT * FROM boosters INNER JOIN character_booster ON character_booster.booster_id=boosters.booster_id INNER JOIN players ON players.character_id = character_booster.character_id WHERE players.username = "qwerty123"
?>

