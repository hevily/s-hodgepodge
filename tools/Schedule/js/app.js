//#ccffcc-绿 #99ccff-蓝  #ffff99-黄 #ff9966-橙 #c0c0c0-灰

var schedule =
	[{
		"starttime": "0000",
		"endtime": "0730",
		"activity": "sleep",
		"color": "#c0c0c0"
	}, {
		"starttime": "0830",
		"endtime": "0845",
		"activity": "day plan",
		"color": "#ccffcc"
	}, {
		"starttime": "0845",
		"endtime": "1045",
		"activity": "work-forenoon",
		"color": "#99ccff"
	}, {
		"starttime": "1045",
		"endtime": "1145",
		"activity": "study-forenoon",
		"color": "#ccffcc"
	}, {
		"starttime": "1330",
		"endtime": "1400",
		"activity": "afternoon nap",
		"color": "#c0c0c0"
	}, {
		"starttime": "1400",
		"endtime": "1600",
		"activity": "work-afternoon",
		"color": "#99ccff"
	}, {
		"starttime": "1600",
		"endtime": "1615",
		"activity": "summarize",
		"color": "#ffff99"
	}, {
		"starttime": "1615",
		"endtime": "1715",
		"activity": "study-afternoon",
		"color": "#ccffcc"
	}, {
		"starttime": "1715",
		"endtime": "1730",
		"activity": "dumbbell",
		"color": "#ffff99"
	}, {
		"starttime": "1730",
		"endtime": "2030",
		"activity": "exercise",
		"color": "#ff9966"
	}, {
		"starttime": "2100",
		"endtime": "2200",
		"activity": "study-evening",
		"color": "#ccffcc"
	}];



//初始化时间表
$(function() {
	var tr = '';
	for (var i = 0; i < 24; i++) {
		for (var j = 0; j < 4; j++) {
			var hour = padLeftZero(i, 2);
			var aid = 'a' + hour + (j === 0 ? '00' : (j * 15));
			if (j === 0)
				tr = '<tr><td rowspan="4" class="title">' + hour + ':00</td>';
			else if (j === 3)
				tr += '<tr class="separator">';
			else
				tr += '<tr>';
			tr += '<td><a id="' + aid + '" name="' + aid + '">15min</a></td></tr>';
		}
		$('.schedule').append(tr);
	}
});

//填充时间表数据
$(function() {
	if (schedule.length > 0) {
		for (var i = 0; i < schedule.length; i++) {
			$('#a' + schedule[i].starttime).after(' <span>' + schedule[i].activity + '</span>');
			var start = parseInt(schedule[i].starttime);
			var end = parseInt(schedule[i].endtime);
			while (true) {
				$('#a' + padLeftZero(start, 4)).closest('td').css('background', schedule[i].color);
				start = start + 15;
				var hour = padLeftZero(start, 4).substr(0, 2);
				var minite = padLeftZero(start, 4).substr(2, 2);
				if (minite === '60') start = parseInt((parseInt(hour) + 1).toString() + '00');
				if (start >= end) break;
			}
		}
	}
});

//定位到当前的活动
$(function() {
	refresh();
	setInterval("refresh()", 40000);
});

function refresh() {
	debugger;
	if (schedule.length > 0) {
		for (var i = 0; i < schedule.length; i++) {
			var start = parseInt(schedule[i].starttime);
			var end = parseInt(schedule[i].endtime);
			var d = new Date();
			var now = parseInt(d.getHours() + '' + padLeftZero(d.getMinutes(),2));
			if (now >= start && now <= end) {
				var activityId = '#a' + padLeftZero(start, 4);
				$('#aPoint').click(function() {
					$("html,body").animate({
						scrollTop: $(activityId).offset().top
					}, 1000);
				});
				$('#aPoint').click();
				while (true) {
					$('#a' + padLeftZero(start, 4)).closest('td').css('background', '#ff66ff');
					start = start + 15;
					var hour = padLeftZero(start, 4).substr(0, 2);
					var minite = padLeftZero(start, 4).substr(2, 2);
					if (minite === '60') start = parseInt((parseInt(hour) + 1).toString() + '00');
					if (start >= end) break;
				}
			}
		}
	}
}

function padLeftZero(num, len) {
	num = num.toString();
	var length = num.length;
	while (length < len) {
		num = "0" + num;
		length++;
	}
	return num;
}

