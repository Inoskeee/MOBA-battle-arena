<?php

	$connect = mysqli_connect("localhost", "root", "root", "unityaccess");

	if(mysqli_connect_errno())
	{
		echo "1"; //: Connection failed
		exit();
	}

	$id = $_POST["item_id"];

	$namecheckquery = "SELECT * FROM swords WHERE sword_id = ".$id.";";

	$namecheck = mysqli_query($connect, $namecheckquery) or die("2"); //: Name check query failed

	$existinginfo = mysqli_fetch_assoc($namecheck);

	echo "0\t".$existinginfo["sword_name"]."\t".$existinginfo["sword_damage"];
?>