// flatMap: https://gist.github.com/samgiles/762ee337dff48623e729
// [B](f: (A) ⇒ [B]): [B]  ; Although the types in the arrays aren't strict (:
Array.prototype.flatMap = function(lambda) {
  return Array.prototype.concat.apply([], this.map(lambda));
};

function capitalize(input) {
  return input[0].toUpperCase() + input.slice(1);
}

function decapitalize(input) {
  return input[0].toLowerCase() + input.slice(1);
}

function normalizeEndpointName(name) {
    if (name.endsWith('-v3'))
      return name.split('-')
        .slice(0, -1)
        .map(capitalize)
            .join('');
    else
        return name.split('-')
        .map(capitalize)
        .join('');
        
}

function normalizeSchemaName(name) {
  return capitalize(name.replace(/DTO/ig, ''));
}

function normalizeArgName(name) {
  var tokens = name.split('_');
  var argName = decapitalize(tokens.map(capitalize).join(''));
  return 'base' === argName ? 'Base' : argName;
}

function normalizePropName(propName, schemaName, type) {
  var tokens = propName.split('_');
    var name = tokens.map(capitalize).join('');
    if (name === schemaName)
        name += typename(type);
  return name;
}

function generateEnumName(propName) {
    var tokens = propName.split('_');
    var name = tokens.map(capitalize).join('');
    return name;
}

function stringifyType(propName, prop, endpoint = null, nullable = false) {
  if (prop.anyOf) {
    prop = prop.anyOf[0];
  }

  var qm = nullable ? '?' : '';
  let refType = prop['$ref'];
    if (refType) {
        var scope = refType.slice(21).split('.');
        var schemaName = normalizeSchemaName(scope.pop());
        var endpoints = scope.map(normalizeEndpointName);
        if (endpoints.length > 0)
            return `Model.${endpoints.join('.')}.${schemaName}` + (endpoints[0] === 'Enums'? qm : '');
  }
  switch (prop.type) {
    case 'boolean': return 'bool' + qm;
    case 'integer': return ('int32' === prop.format ? 'int' : 'long') + qm;
    case 'number': return prop.format + qm;
    case 'array': return stringifyType(propName, prop.items, endpoint) + '[]';
    case 'string':
        var enumerations = prop['enum'];
        if (enumerations) {
            return generateEnumName(propName)+qm;
        }
        return 'string';
    case 'object':
      return 'IDictionary<' + stringifyType(propName, prop['x-key'], endpoint) + ', ' +
        stringifyType(propName, prop.additionalProperties, endpoint) + '>';
    default: return prop.type;
  }
}

function typename(type) {
    if (type['$ref']) {
        var scope = type['$ref'].slice(21).split('.');
        var schemaName = normalizeSchemaName(scope.pop());
        return schemaName;
    }
    return type.type;
}

function formatJsonProperty(name) {
  return `[JsonProperty(\"${name}\")]`;
}

module.exports = {
  capitalize,
  decapitalize,
  normalizeEndpointName,
  normalizeSchemaName,
  normalizeArgName,
  normalizePropName,
  generateEnumName,

  stringifyType,
  formatJsonProperty
};