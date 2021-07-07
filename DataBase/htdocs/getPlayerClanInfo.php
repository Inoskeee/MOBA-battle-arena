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

	$membersquery = "SELECT username FROM players INNER JOIN player_clans ON player_clans.player_id=players.id INNER JOIN clans ON clans.clan_id=player_clans.clan_id WHERE clans.clan_id = ".$existinginfo["clan_id"].";";

	$memberscheck = mysqli_query($connect, $membersquery) or die("2");

	$names = "";
	while ($members = mysqli_fetch_assoc($memberscheck)) 
	{
		$names .= $members["username"]."\t";
	}

	$nameleaderquery = "SELECT * FROM players WHERE id = ".$existinginfo["clan_leader"].";";
	$leadercheck = mysqli_query($connect, $nameleaderquery) or die("2"); //: Name check query failed

	$existingleader = mysqli_fetch_assoc($leadercheck);

	echo "0\t".$existinginfo["clan_name"]."\t".$existinginfo["clan_raiting"]."\t".$existingleader["username"]."\t".$names;
?>