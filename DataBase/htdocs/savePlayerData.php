<?php
	$connect = mysqli_connect("localhost", "root", "root", "unityaccess");

	if(mysqli_connect_errno())
	{
		echo "1"; //: Connection failed
		exit();
	}

	$username = $_POST["username"];
	$head = $_POST["head"];
	$body = $_POST["body"];
	$leg = $_POST["leg"];
	$sword = $_POST["sword"];
	$bow = $_POST["bow"];


	$namecheckquery = "SELECT * FROM players WHERE username = '".$username."';";

	$namecheck = mysqli_query($connect, $namecheckquery) or die("2"); //: Name check query failed
	$existplayerid = mysqli_fetch_assoc($namecheck);


	$updateuserquery = "UPDATE characters SET head = ".$head.", body = ".$body.", legs = ".$leg.", sword = ".$sword.", bow = ".$bow." WHERE characters.character_id = ".$existplayerid["character_id"].";";
	mysqli_query($connect, $updateuserquery) or die("5"); //: Insert player failed


	echo "0" //: No errors

?>