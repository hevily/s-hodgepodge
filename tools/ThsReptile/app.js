var fs = require('fs');
var cheerio = require('cheerio');
var superagent = require('superagent');
var nodemailer = require('nodemailer');

var ths_statistics_urls =
	[
		'http://moni.10jqka.com.cn/paihang.shtml', //总盈利率排名
		'http://moni.10jqka.com.cn/yyl.shtml', //月盈利率排名
		'http://moni.10jqka.com.cn/zyl.shtml', //周盈利率排名
		'http://moni.10jqka.com.cn/ryl.shtml', //日盈利率排名
		'http://moni.10jqka.com.cn/xgcg.shtml', //选股成功率排名
		'http://moni.10jqka.com.cn/zzcs.shtml', //被追踪数排名
		'http://moni.10jqka.com.cn/dw.shtml' //段位排名
	];

var ths_personal_urls = [];
var ths_result_urls = [];

var ths_personal_urls_count = 0;
var ths_result_urls_count = 0;

function getPersonalUrls() {
	ths_statistics_urls.forEach(function (url) {
		(function (url) {
			superagent
				.get(url)
				.end(function (err, res) {
					ths_personal_urls_count++;
					//if (ths_personal_urls_count = 1) getResultUrls();
					//if (ths_personal_urls_count > 1) return;
					if (err) { } //todo:错误处理
					var $ = cheerio.load(res.text);
					var $trs = $('#myTab_Content' + 0).children().last().children();
					//console.log($trs.length);
					$trs.each(function (index, element) {
						ths_personal_urls.push($(element).children().last().find('a').attr('href'));
					});
					if (ths_personal_urls_count === ths_statistics_urls.length - 1) {
						ths_personal_urls = unique(ths_personal_urls); //去掉重复的数据	
						getResultUrls();
					}
				});
		})(url);
	});
}

function getResultUrls() {
	ths_personal_urls.forEach(function (url) {
		(function (url) {
			superagent
				.get(url)
				.end(function (err, res) {
					ths_result_urls_count++;
					//if (ths_result_urls_count > 1) return;
					if (err) { } //todo:错误处理
					var $ = cheerio.load(res.text);
					var $trs = $('#ccqk_tbl tbody').children();
					var todayDate = new Date();
					todayDate.setDate(todayDate.getDate() - 2);
					$trs.each(function (index, element) {
						var strOrderDate = $(element).children().eq(3).find('span').text();
						var orderDate = new Date(strOrderDate.replace('-', '/'));
						if (orderDate > todayDate) {
							ths_result_urls.push(url);
						}
					});
					if (ths_result_urls_count === ths_personal_urls.length - 1) {
						ths_result_urls = unique(ths_result_urls); //去掉重复的数据
						ths_result_urls.forEach(function (url) {
							console.log(url);
						});
						//sendMail();
					}
				});
		})(url);
	});
}

function unique(arr) {
    var result = [], hash = {};
    for (var i = 0, elem; (elem = arr[i]) != null; i++) {
        if (!hash[elem]) {
            result.push(elem);
            hash[elem] = true;
        }
    }
	return result;
}


function sendMail() {
	var transporter = nodemailer.createTransport({
		service: '163',
		auth: {
			user: 'thsreptile@163.com',
			pass: 'gxzjgijphwspwrko'
		}
	});
	var text = '';
	ths_personal_urls.forEach(function (value, index) {
		text = ',' + value;
	});
	transporter.sendMail({
		from: '',
		to: '',
		subject: '同花顺模拟炒股爬虫',
		text: text
	});
}

//sendMail();
getPersonalUrls();


	



