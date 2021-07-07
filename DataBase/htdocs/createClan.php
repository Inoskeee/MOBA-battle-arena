<?php
	$connect = mysqli_connect("localhost", "root", "root", "unityaccess");

	if(mysqli_connect_errno())
	{
		echo "1"; //: Connection failed
		exit();
	}

	$clanName = $_POST["clanName"];
	$username = $_POST["username"];


	$clancheckquery = "SELECT clan_name FROM clans WHERE clan_name = '".$clanName."';";

	$clancheck = mysqli_query($connect, $clancheckquery) or die("2"); //: Name check query failed

	if(mysqli_num_rows($clancheck) > 0)
	{
		echo "3"; //: This clan name already exsists
		exit();
	}

	if(!preg_match("/^[a-z0-9-_]{2,20}$/i", $clanName))
	{
  		echo "4"; //: Clan name is not valid
		exit();
	}


	$namecheckquery = "SELECT * FROM players WHERE username = '".$username."';";

	$namecheck = mysqli_query($connect, $namecheckquery) or die("2"); //: Name check query failed
	$existplayerid = mysqli_fetch_assoc($namecheck);

	$namecheckquery = "SELECT * FROM characters WHERE characters.character_id = ".$existplayerid["character_id"].";";

	$namecheck = mysqli_query($connect, $namecheckquery) or die("2"); //: Name check query failed
	$existplayerid = mysqli_fetch_assoc($namecheck);

	//Add clan


	$insertuserquery = "INSERT INTO clans (clan_name, clan_raiting, clan_leader) VALUES ('".$clanName."', ".$existplayerid["score"].", ".$existplayerid["character_id"].");";

	mysqli_query($connect, $insertuserquery) or die("5"); //: Insert player failed


	$clan_id = mysqli_query($connect, "SELECT clan_id FROM clans WHERE clan_name = '".$clanName."';") or die("2");
	$existclanid = mysqli_fetch_assoc($clan_id);


	$player_id = mysqli_query($connect, "SELECT id FROM players WHERE username = '".$username."';") or die("2");
	$existplayerid = mysqli_fetch_assoc($player_id);



	$insertuserquery = "INSERT INTO player_clans (clan_id, player_id) VALUES (".$existclanid["clan_id"].", ".$existplayerid["id"].");";

	mysqli_query($connect, $insertuserquery) or die("5"); //: Insert player failed

	echo "0"; //: No errors


?>