<?php
	$connect = mysqli_connect("localhost", "root", "root", "unityaccess");

	if(mysqli_connect_errno())
	{
		echo "1"; //: Connection failed
		exit();
	}

	$username = $_POST["username"];
	$score = $_POST["score"];
	$clanName = $_POST["clanName"];

	$namecheckquery = "SELECT * FROM players WHERE username = '".$username."';";

	$namecheck = mysqli_query($connect, $namecheckquery) or die("2"); //: Name check query failed
	$existplayerid = mysqli_fetch_assoc($namecheck);


	$updateuserquery = "UPDATE characters SET score = ".$score." WHERE characters.character_id = ".$existplayerid["character_id"].";";
	mysqli_query($connect, $updateuserquery) or die("5"); //: Insert player failed

	if($clanName != "")
	{
		$namecheckquery = "SELECT * FROM clans WHERE clan_name = '".$clanName."';";

		$namecheck = mysqli_query($connect, $namecheckquery) or die("2"); //: Name check query failed

		$existinginfo = mysqli_fetch_assoc($namecheck);

		$updateclanquery = "UPDATE clans SET clan_raiting = ".($existinginfo["clan_raiting"]+20)." WHERE clans.clan_id = ".$existinginfo["clan_id"].";";
		mysqli_query($connect, $updateclanquery) or die("5"); //: Insert player failed
	}


	echo "0"; //: No errors

?>