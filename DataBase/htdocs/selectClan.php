<?php

	$connect = mysqli_connect("localhost", "root", "root", "unityaccess");

	if(mysqli_connect_errno())
	{
		echo "1"; //: Connection failed
		exit();
	}

	$name = $_POST["clanName"];

	$namecheckquery = "SELECT * FROM clans WHERE clan_name = '".$name."';";

	$namecheck = mysqli_query($connect, $namecheckquery) or die("2"); //: Name check query failed

	$existinginfo = mysqli_fetch_assoc($namecheck);

	$nameleaderquery = "SELECT * FROM players WHERE id = ".$existinginfo["clan_leader"].";";
	$leadercheck = mysqli_query($connect, $nameleaderquery) or die("2"); //: Name check query failed

	$existingleader = mysqli_fetch_assoc($leadercheck);

	echo "0\t".$existinginfo["clan_name"]."\t".$existinginfo["clan_raiting"]."\t".$existingleader["username"];
?>