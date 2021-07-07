<?php
	$connect = mysqli_connect("localhost", "root", "root", "unityaccess");

	if(mysqli_connect_errno())
	{
		echo "1"; //: Connection failed
		exit();
	}

	$username = $_POST["name"];
	$password = $_POST["password"];
	$email = $_POST["email"];

	$namecheckquery = "SELECT username FROM players WHERE username = '".$username."';";

	$namecheck = mysqli_query($connect, $namecheckquery) or die("2"); //: Name check query failed

	if(mysqli_num_rows($namecheck) > 0)
	{
		echo "3"; //: This name already exsists
		exit();
	}

	if(!preg_match("/^(?:[a-z0-9]+(?:[-_.]?[a-z0-9]+)?@[a-z0-9_.-]+(?:\.?[a-z0-9]+)?\.[a-z]{2,5})$/i", $email))
	{
  		echo "4"; //: Email is not valid
		exit();
	}

	//Add user
	$salt = "\$5\$rounds=5000\$"."steamedhams".$username."\$";
	$hash = crypt($password, $salt);

	$idquery = mysqli_fetch_assoc(mysqli_query($connect, "SELECT MAX(id) FROM players"));

	if($idquery['MAX(id)'] == null)
	{
		$idquery['MAX(id)'] = 0;
	}


	$insertuserquery = "INSERT INTO characters (score, head, body, legs, sword, bow) VALUES ('0', '1', '2', '3', '1', '1');";
	mysqli_query($connect, $insertuserquery) or die("5"); //: Insert player failed

	$insertuserquery = "INSERT INTO players (username, hash, salt, email, character_id) VALUES ('".$username."', '".$hash."', '".$salt."', '".$email."', '".($idquery['MAX(id)']+1)."');";

	mysqli_query($connect, $insertuserquery) or die("5"); //: Insert player failed



	echo "0"; //: No errors

	/*
	Объединение:
	SELECT players.*, characters.*
	FROM players INNER JOIN characters
	ON players.character_id = characters.character_id
	*/

?>