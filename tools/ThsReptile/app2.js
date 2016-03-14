var fs = require('fs');
var cheerio = require('cheerio');
var superagent = require('superagent');
var nodemailer = require('nodemailer');

var ths_personal_urls =
	[
		'http://moni.10jqka.com.cn/1642662',
	];

var ths_result_urls = [];
var ths_result_urls_count = 0;

// 默认查询当天的交易
var queryDate = new Date();
queryDate.setDate(queryDate.getDate() - 1); 

function getResultUrls() {
	ths_personal_urls.forEach(function (url) {
		(function (url) {
			console.log(url);
			superagent
				.get(url)
				.end(function (err, res) {
					ths_result_urls_count++;
					if (err) { console.log(err); return; } //todo:错误处理
					var $ = cheerio.load(res.text);
					var $trs = $('#jyjl_tbl tbody').children();
					//console.log($trs);
					var strOrderDate = $trs.eq(1).children().eq(1).find('span').text();
					//console.log($trs.children().eq(1));
					console.log($trs.eq(1).children().eq(1).children().text());
					var orderDate = new Date(strOrderDate.replace('-', '/'));
					if (orderDate > queryDate) {
						ths_result_urls.push(url);
					}
					if (ths_result_urls_count === ths_personal_urls.length) {
						ths_result_urls = unique(ths_result_urls); //去掉重复的数据
						ths_result_urls.forEach(function (url) {
							console.log(url);
						});
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

getResultUrls();


//sendMail();


	



