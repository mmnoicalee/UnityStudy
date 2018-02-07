<?php


// 1. GET방식으로 입력 받은 아이디 입력(SELECT) 
	// 2.[서] 입력 받은 아이디로 로그인
		// 2.1 로그인 실패
			// 2.1.1 아이디 추가(INSERT)
			// 2.1.2 다시 1번 시도 (SELECT)
			// 2.1.3 로그인 정보 리턴 (RESPONSE)
		// 2.2 로그인 성공 
			// 2.2.1 로그인 정보 리턴 (RESPONSE)

	function select_info($nick_name, $connect, $response_data)
	{
		$sql = "SELECT * FROM clickinfo_tbb WHERE"." nick_name='".$nick_name."'";

		// 쿼리 실행
		$result = mysqli_query($connect, $sql);

		if($result->num_rows > 0){
			while ($row = mysqli_fetch_assoc($result)) 
			{
				$response_data["RESULT"] = "LOAD_SUCCESS";
				$response_data["USER_INFO"] = $row;
						
				echo json_encode($response_data);
				mysqli_close($connect);
				exit();
			}
		}
	}


	$nick_name = $_GET["nick_name"];

	$response_data = array(
		"RESULT" => "LOAD_FAIL",
		"USER_INFO" => null
	);
	// DBMS연결 
	$connect = mysqli_connect(
		"127.0.0.1", "root", "autoset", "clickergame_dab"
	);

	select_info($nick_name, $connect, $response_data);
	//유저 정보 추가 쿼리 작성
	$sql = "INSERT INTO clickinfor_tbb(nick_name) VALUES('".$nick_name."');";
	// 쿼리 실행
	mysqli_query($connect, $sql);
	// 쿼리 실행과 리턴
	$result = mysqli_affected_rows($connect);
	// 유저 정보 추가 성공 체크

	if ($result > 0){
		select_info($nick_name, $connect, $response_data);
	}

	echo json_encode($response_data);
	mysqli_close($connect);






