<?php

	$connect = mysqli_connect("localhost", "root", "root", "unityaccess");

	if(mysqli_connect_errno())
	{
		echo "1"; //: Connection failed
		exit();
	}

	$id = $_POST["item_id"];

	$namecheckquery = "SELECT item_id, item_name, armor, body_part_name FROM equipments INNER JOIN body_parts ON equipments.body_part=body_parts.body_part_id WHERE item_id = ".$id.";";

	$namecheck = mysqli_query($connect, $namecheckquery) or die("2"); //: Name check query failed

	$existinginfo = mysqli_fetch_assoc($namecheck);

	echo "0\t".$existinginfo["item_name"]."\t".$existinginfo["armor"]."\t".$existinginfo["body_part_name"];
?>