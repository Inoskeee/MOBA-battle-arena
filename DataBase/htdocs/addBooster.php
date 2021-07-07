<?php
	$connect = mysqli_connect("localhost", "root", "root", "unityaccess");

	if(mysqli_connect_errno())
	{
		echo "1"; //: Connection failed
		exit();
	}

	$username = $_POST["username"];
	$booster_id = $_POST["booster_id"];


	$player_id = mysqli_query($connect, "SELECT character_id FROM players WHERE username = '".$username."';") or die("2");
	$existplayerid = mysqli_fetch_assoc($player_id);



	$insertuserquery = "INSERT INTO character_booster (character_id, booster_id) VALUES (".$existplayerid["character_id"].", ".$booster_id.");";

	mysqli_query($connect, $insertuserquery) or die("5"); //: Insert player failed

	echo "0"; //: No errors


?>