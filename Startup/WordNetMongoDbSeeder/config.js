var fs = require('fs'),
    content = fs.readFileSync('config.json', 'utf8'),
    config = JSON.parse(content);

module.exports = config;
