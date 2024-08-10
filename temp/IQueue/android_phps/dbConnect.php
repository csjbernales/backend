<!DOCTYPE html>
<?php
	//Change the values according to your database
	
	define('HOST','localhost');
	define('USER','foobar2018');
	define('PASS','foobar2018');
	define('DB','thesis_db');
	
	$con = mysqli_connect(HOST,USER,PASS,DB) or die('Unable to Connect');
	