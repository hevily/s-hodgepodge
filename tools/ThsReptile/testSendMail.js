var nodemailer = require('nodemailer');

var transporter = nodemailer.createTransport({
	service: '163',
	auth: {
		user: 'thsreptile@163.com',
		pass: 'gxzjgijphwspwrko'
	}
});

transporter.sendMail({
	from: 'thsreptile@163.com',
	to: 'stone0090@qq.com',
	subject: '同花顺模拟炒股爬虫_测试邮件。',
	text: '同花顺模拟炒股爬虫_测试邮件。'
});
