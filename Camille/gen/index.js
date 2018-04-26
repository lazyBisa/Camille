const util = require('util');
const fs = require('fs');
fs.readFileAsync = util.promisify(fs.readFile);
fs.writeFileAsync = util.promisify(fs.writeFile);

const doT = require('dot');
const doT2 = require('dot');
const glob = require('glob-promise');
const dotUtils = require('./dotUtils.js');

const log = a => { console.log(a); return a; };
const suffix = '.dot';

const CommonSettings = {
  evaluate: /\r?\n?\{\{([\s\S]+?)\}\}/g,
  interpolate: /\r?\n?\{\{=([\s\S]+?)\}\}/g,
  encode: /\r?\n?\{\{!([\s\S]+?)\}\}/g,
  use: /\r?\n?\{\{#([\s\S]+?)\}\}/g,
  define: /\r?\n?\{\{##\s*([\w\.$]+)\s*(\:|=)([\s\S]+?)#\}\}/g,
  conditional: /\r?\n?\{\{\?(\?)?\s*([\s\S]*?)\s*\}\}/g,
  iterate: /\r?\n?\{\{~\s*(?:\}\}|([\s\S]+?)\s*\:\s*([\w$]+)\s*(?:\:\s*([\w$]+))?\s*\}\})/g,
  varname: 'spec',
  strip: false,
  append: false,
  selfcontained: false
};

global.require = require;

const outDir = '../src-gen/';
const templatesDir = process.cwd() + '/templates/';

var spec = require('./.spec.json');

const files = ['RiotApi.cs.dot', 'RiotApiConfig.cs.dot', 'Endpoints.cs.dot'];

//glob.promise(`**/*${suffix}`, { ignore: ['**/node_modules/**'], root: templatesDir})
//    .then(files =>
Promise.all(files
    .map(log)
    .map(file => fs.readFileAsync(templatesDir + file, 'utf8')
      .then(input => doT.template(input, CommonSettings)(spec))
      .then(output => fs.writeFileAsync(`${outDir}${file.slice(0, -suffix.length)}`, output, 'utf8'))
    )
  ).catch(console.error);

const ModelClassSettings = {
    evaluate: /\r?\n?\{\{([\s\S]+?)\}\}/g,
    interpolate: /\r?\n?\{\{=([\s\S]+?)\}\}/g,
    encode: /\r?\n?\{\{!([\s\S]+?)\}\}/g,
    use: /\r?\n?\{\{#([\s\S]+?)\}\}/g,
    define: /\r?\n?\{\{##\s*([\w\.$]+)\s*(\:|=)([\s\S]+?)#\}\}/g,
    conditional: /\r?\n?\{\{\?(\?)?\s*([\s\S]*?)\s*\}\}/g,
    iterate: /\r?\n?\{\{~\s*(?:\}\}|([\s\S]+?)\s*\:\s*([\w$]+)\s*(?:\:\s*([\w$]+))?\s*\}\})/g,
    varname: 'endpoint, schemaName, schema, version',
    strip: false,
    append: false,
    selfcontained: false
};

establish(`${outDir}Model/`);
fs.readFileAsync(templatesDir + 'ModelClasses.cs.dot', 'utf8')
    .then(input => doT.template(input, ModelClassSettings))
    .then(classBuilder =>
        Promise.all(Object.entries(spec.components.schemas)
            .filter(([schemaKey, schema]) => 'Error' !== schemaKey)
            .map(([schemaKey, schema]) => {
                const schemaSplit = schemaKey.split('.');
                const endpoint = dotUtils.normalizeEndpointName(schemaSplit[0]);
                const schemaName = dotUtils.normalizeSchemaName(schemaSplit[1]);
                console.log(`${endpoint}/${schemaName}`);

                establish(`${outDir}Model/${endpoint}/`);
                const output = classBuilder(endpoint, schemaName, schema, spec.info.version);
                return fs.writeFileAsync(`${outDir}Model/${endpoint}/${schemaName}.cs`, output, 'utf8');
            })))
    .catch(console.error);


function establish(directory) {
    try {
        fs.mkdirSync(directory);
    } catch (e) {
        if (e.code !== 'EEXIST')
            console.error(e);
    }
}