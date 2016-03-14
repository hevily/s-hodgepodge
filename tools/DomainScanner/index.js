var fs = require('fs');
var superagent = require('superagent');
var cheerio = require('cheerio');

var count = 0;
var listCharacter1 = ['a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z'];
var listCharacter2 = ['0', '1', '2', '3', '4', '5', '6', '7', '8', '9'];
var listCharacter3 = ['-'];

// // 扫描所有3位数包含“字母”、“数字”的.com域名(已被全部注册)
// scanningDomain(perm(listCharacter1.concat(listCharacter2), 3), '.com', 0);

// // 扫描所有3位数包含“字母”、“数字”的.net域名
// scanningDomain(perm(listCharacter1.concat(listCharacter2), 3), '.net', 1815);

// // 扫描所有3位数包含“字母”、“数字”的.me域名
// scanningDomain(perm(listCharacter1.concat(listCharacter2), 3), '.me', 0);

// // 扫描所有4位数包含“字母”的.com域名
// scanningDomain(perm(listCharacter1, 4), '.com', 1775);

// // 扫描所有4位数包含“字母”的.net域名
// scanningDomain(perm(listCharacter1, 4), '.net', 0);

// 所有扫描失败的域名保存在 domain_error.txt 中，可以手工在 http://wanwang.aliyun.com/domain/searchresult/ 进行查询。


//校验域名是否已注册
function scanningDomain(arr, suffix, start) {
	if (suffix === undefined) suffix = '.com';
	if (start === undefined) start = 0;
	if (start === arr.length) {
		console.log('scanning finish.');
		return;
	}
	var name = arr[start];
	var url = 'http://panda.www.net.cn/cgi-bin/check.cgi?area_domain=' + name + suffix;
	console.log(start);
	console.log(url);
	(function (url_copy, name_copy, arr_copy, start_copy, suffix_copy) {
		superagent
			.get(url_copy)
			.end(function (err, sres) {
				try {
					console.log(sres.text); // 输出查询结果。
					if (sres.text.indexOf('<returncode>200</returncode>') > -1) {
						if (sres.text.indexOf('<original>210 : Domain name is available</original>') > -1) {
							fs.appendFileSync('domain' + suffix_copy + name_copy.length + '.txt', name_copy + suffix_copy + '\r\n', 'utf8');
						}
					} else {
						fs.appendFileSync('domain_error' + suffix_copy + name_copy.length + '.txt', name_copy + suffix_copy + '\r\n', 'utf8');
					}
				} catch (error) {
					fs.appendFileSync('domain_error' + suffix_copy + name_copy.length + '.txt', name_copy + suffix_copy + '\r\n', 'utf8');
				}
				setTimeout(function () {
					scanningDomain(arr_copy, suffix_copy, ++start_copy);
				}, 2000); // 2000毫秒查询一次，速度可自行修改，速度小于500毫秒会被短时间禁止查询。
			})
	})(url, name, arr, start, suffix)
}

//排列算法，调用示例：perm(data, 3)
function perm(arr, count, temp, result) {
	if (temp === undefined) temp = '';
	if (result === undefined) result = [];
	arr.forEach(function (ele) {
		var name = temp + ele;
		if (count === 1) {
			result.push(name);
			console.log(name);
		} else {
			perm(arr, count - 1, name, result);
		}
	}, this);
	return result;
}
