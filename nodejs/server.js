var fs = require('fs');
var url = require('url');
var express = require('express')
  , form = require('connect-form');
var mysql = require('mysql');
var client = mysql.createClient({
  user: 'root',
  password: ''
});

var app = express.createServer(
  // connect-form (http://github.com/visionmedia/connect-form)
  // middleware uses the formidable middleware to parse urlencoded
  // and multipart form data
  form({ keepExtensions: true })
);
app.get('/', function(request, response){
        console.log('GET');
        retrieveData(request, response);
	});
	
app.post('/', function(request, response, next){
	response.end();
	postData(request,next);
	});

var postData = function (request,next){
	var file = null;
	request.form.complete(function(err, fields, files){
		if (err) {
			next(err);
		} else {
			updateDatabase(fields);
			file = files.file;
		}
		console.log(fields);
		console.log(file);
		moveFile(file);
	});
}

var moveFile = function (file){
/*	var inp = fs.createReadStream(file.path);
	inp.setEncoding('binary');
	var inptext = new Array();  
	inp.on('data', function (data) {
		inptext.push(data);
	});
	inp.on('end', function (close) {
		fs.writeFileSync("/home/ec2-user/CSE520S/nodejs/images/"+file.name,inptext.join('') );
	});*/
	fs.rename(file.path, "/home/ec2-user/CSE520S/nodejs/images/"+file.name,function(err){
		console.log(err);
	});
}

var updateDatabase = function (elements){
	client.query('USE cse520S', function(error, results) {
		if(error) {
			console.log('ClientConnectionReady Error: ' + error.message);
			client.end();
			return;
		}
		client.query("insert into sensor Set type = ?, value = ?, lat = ?, lon = ?" , [elements.type, elements.value, elements.lat, elements.lon]);
	});    
}

var retrieveData = function (request, response){
    var items = new Array();
    var params = url.parse(request.url).query;
    var type = params.split('=')[1];
    client.query('USE cse520S', function(error, results) {
        if(error) {
            console.log('ClientConnectionReady Error: ' + error.message);
            client.end();
            return;
        }
        client.query("SELECT * FROM sensor where type='"+type+"'", function selectSensorData(error, results, fields) {
            if (error) {
                console.log('GetData Error: ' + error.message);
                client.end();
                return;
            }
            for (var i = 0; i < results.length; i++){
                items.push({latitude:results[i]['lat'],
                            longitude:results[i]['lon'],
                            type:results[i]['type'],
                            value:results[i]['value']});
            }
            var sensorData = {items: items};
            var body = JSON.stringify(sensorData);
            response.writeHead(200, {
                'Content-Type': 'text/json',
                'Access-Control-Allow-Origin': '*',
                'Content-Length': body.length});
            response.write(body);
            response.end();
        });

    });
}
app.listen(1337);
console.log('Express app started on http://127.0.0.1:1337/');
