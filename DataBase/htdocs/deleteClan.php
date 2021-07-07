<?php
	$connect = mysqli_connect("localhost", "root", "root", "unityaccess");

	if(mysqli_connect_errno())
	{
		echo "1"; //: Connection failed
		exit();
	}

	$clanName = $_POST["clanName"];

	$clan_id = mysqli_query($connect, "SELECT clan_id FROM clans WHERE clan_name = '".$clanName."';") or die("2");
	$existclanid = mysqli_fetch_assoc($clan_id);



	$removeclanquery = "DELETE FROM clans WHERE clans.clan_id = ".$existclanid["clan_id"].";";

	mysqli_query($connect, $removeclanquery) or die("5"); //: Insert player failed

	echo "0"; //: No errors
?>