<?php

 include 'DatabaseConfig.php';

    $connection = mysqli_connect($HostName,$HostUser,$HostPass,$DatabaseName) or die("Error " . mysqli_error($connection));
$username=$_GET['phone'];
$query="SELECT * FROM accounts where username='{$username}'";

$result = mysqli_query($connection, $query);
while(($row = mysqli_fetch_assoc($result)) == true){
	$data[]=$row;
}
echo json_encode($data);


	
?>
