<?php

	$connect = mysqli_connect("localhost", "root", "root", "unityaccess");

	if(mysqli_connect_errno())
	{
		echo "1"; //: Connection failed
		exit();
	}

	$namecheckquery = "SELECT * FROM clans;";

	$namecheck = mysqli_query($connect, $namecheckquery) or die("2"); //: Name check query failed

	$names = "";
	while ($existinginfo = mysqli_fetch_assoc($namecheck)) 
	{
		$names .= $existinginfo["clan_name"]."\t";
	}


	echo "0\t".$names;
?>