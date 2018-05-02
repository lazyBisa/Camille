var dotUtils = require('./dotUtils.js');


// fixes certain schema elements, like missing enum types, onOf flags etc.
function fixSchema(spec) {
    spec.info.version = '1';


    let localeEnum = makeEnum(spec.paths['/lol/static-data/v3/champions'].get.parameters.find(p => p.name === 'locale').schema.enum);
    addType('enums', 'Locale', localeEnum, spec);
    spec.paths['/lol/static-data/v3/champions'].get.parameters.find(p => p.name === 'locale').schema = makeTypeRef('enums', 'Locale');
    spec.paths['/lol/static-data/v3/champions/{id}'].get.parameters.find(p => p.name === 'locale').schema = makeTypeRef('enums', 'Locale');
    spec.paths['/lol/static-data/v3/items'].get.parameters.find(p => p.name === 'locale').schema = makeTypeRef('enums', 'Locale');
    spec.paths['/lol/static-data/v3/items/{id}'].get.parameters.find(p => p.name === 'locale').schema = makeTypeRef('enums', 'Locale');
    spec.paths['/lol/static-data/v3/language-strings'].get.parameters.find(p => p.name === 'locale').schema = makeTypeRef('enums', 'Locale');
    spec.paths['/lol/static-data/v3/maps'].get.parameters.find(p => p.name === 'locale').schema = makeTypeRef('enums', 'Locale');
    spec.paths['/lol/static-data/v3/masteries'].get.parameters.find(p => p.name === 'locale').schema = makeTypeRef('enums', 'Locale');
    spec.paths['/lol/static-data/v3/masteries/{id}'].get.parameters.find(p => p.name === 'locale').schema = makeTypeRef('enums', 'Locale');
    spec.paths['/lol/static-data/v3/profile-icons'].get.parameters.find(p => p.name === 'locale').schema = makeTypeRef('enums', 'Locale');
    spec.paths['/lol/static-data/v3/runes'].get.parameters.find(p => p.name === 'locale').schema = makeTypeRef('enums', 'Locale');
    spec.paths['/lol/static-data/v3/runes/{id}'].get.parameters.find(p => p.name === 'locale').schema = makeTypeRef('enums', 'Locale');
    spec.paths['/lol/static-data/v3/summoner-spells'].get.parameters.find(p => p.name === 'locale').schema = makeTypeRef('enums', 'Locale');
    spec.paths['/lol/static-data/v3/summoner-spells/{id}'].get.parameters.find(p => p.name === 'locale').schema = makeTypeRef('enums', 'Locale');

    let cqEnum = makeEnum(spec.paths['/lol/league/v3/challengerleagues/by-queue/{queue}'].get.parameters.find(p => p.name === 'queue').schema.enum);
    addType('enums', 'CompetitiveQueue', cqEnum, spec);
    spec.paths['/lol/league/v3/challengerleagues/by-queue/{queue}'].get.parameters.find(p => p.name === 'queue').schema = makeTypeRef('enums', 'CompetitiveQueue');
    spec.paths['/lol/league/v3/masterleagues/by-queue/{queue}'    ].get.parameters.find(p => p.name === 'queue').schema = makeTypeRef('enums', 'CompetitiveQueue');

    extractEnum('lol-static-data-v3', 'MasteryDto', 'masteryTree', 'RunesReforgedPath', spec);
    extractEnum('match-v3', 'MatchEventDto', 'type', 'MatchEventTypes', spec);
    extractEnum('spectator-v3', 'FeaturedGameInfo', 'gameType', 'gameType', spec);
    extractEnum('spectator-v3', 'FeaturedGameInfo', 'gameMode', 'gameMode', spec, ' In the spectator endpoint since the enum is not used anywhere else right now (possible usages not validated)');

    var pickRef = extractEnum('tournament-stub-v3', 'TournamentCodeParameters', 'pickType', 'PickType', spec);
    setTypeRef(spec, 'tournament-v3', 'TournamentCodeParameters', 'pickType', pickRef);
    setTypeRef(spec, 'tournament-v3', 'TournamentCodeUpdateParameters', 'pickType', pickRef);
    var mapRef = extractEnum('tournament-stub-v3', 'TournamentCodeParameters', 'mapType', 'MapType', spec);
    setTypeRef(spec, 'tournament-v3', 'TournamentCodeParameters', 'mapType', mapRef);
    setTypeRef(spec, 'tournament-v3', 'TournamentCodeUpdateParameters', 'mapType', mapRef);
    var spectateRef = extractEnum('tournament-stub-v3', 'TournamentCodeParameters', 'spectatorType', 'SpectatorType', spec);
    setTypeRef(spec, 'tournament-v3', 'TournamentCodeParameters', 'spectatorType', spectateRef);
    setTypeRef(spec, 'tournament-v3', 'TournamentCodeUpdateParameters', 'spectatorType', spectateRef);
    var regionRef = extractEnum('tournament-stub-v3', 'ProviderRegistrationParameters', 'region', 'Region', spec);
    setTypeRef(spec, 'tournament-v3', 'TournamentCodeDTO', 'region', regionRef);
    setTypeRef(spec, 'tournament-v3', 'ProviderRegistrationParameters','region', regionRef);


    for (let [path, propname, proptype] of enumtypesInSpec(spec))
        break; //console.log(`${path.length} ${path.join('.')}.${propname} \t: ${proptype.type} ${proptype.enum}`);
}

function extractEnum(endpoint, schema, property, newName, spec, description = '') {
    let enumtype = makeEnum(spec.components.schemas[`${endpoint}.${schema}`].properties[property].enum);
    enumtype.description += description;
    let ref = addType(endpoint, newName, enumtype, spec);
    setTypeRef(spec, endpoint, schema, property, ref);
    return ref;
}


function setTypeRef(spec, endpoint, schema, property, ref) {
    spec.components.schemas[`${endpoint}.${schema}`].properties[property] = makeTypeRefR(ref);
}

function* typeRootsInSpec(spec) {
    yield* schemaTypes(spec);
    yield* endpointInplaceTypes(spec);
}

function* schemaTypes(spec) {
    for (let [schemaKey, schema] of Object.entries(spec.components.schemas))
        if (schemaKey !== 'Error')
            yield [['components', 'schemas'], schemaKey, schema];
}
function* endpointInplaceTypes(spec) {
    for (let [pathKey, path] of Object.entries(spec.paths))
        if (path.get && path.get.parameters && path.get.parameters.length)
            for (let param of path.get.parameters)
                yield [['paths', pathKey], param.name, param.schema];
}

function* typesInSpec(spec) {
    for (let [path, propname, prop] of typeRootsInSpec(spec)) {
        yield [path, propname, prop];
        yield* subtypesInProperty(propname, prop, path);
    }
}

function* enumtypesInSpec(spec) {
    for (let [path, propname, prop] of typeRootsInSpec(spec))
        yield* enumtypesInProperty(propname, prop, path);
}

function* enumtypesInProperty(propname, proptype, path) {
    switch (proptype.type) {
        case 'boolean':
        case 'integer':
        case 'number':
            break;
        case 'array':
            yield* enumtypesInProperty(propname, proptype.items, path);
            break;
        case 'string':
            if (proptype['enum']) {
                yield [path, propname, proptype];
            }
            break;
        case 'object':
            if (proptype['x-key']) {
                yield* enumtypesInProperty(propname, proptype['x-key'], path);
                yield* enumtypesInProperty(propname, proptype.additionalProperties, path);
            } else {
                for (let [pn, pt] of Object.entries(proptype.properties)) {
                    path.push(propname);
                    yield* enumtypesInProperty(pn, pt, path);
                    path.pop();
                }
            }
            break;
        default: break;
    }
}

function* subtypesInProperty(propname, proptype, path) {
    switch (proptype.type) {
        case 'boolean':
        case 'integer':
        case 'number':
        case 'string':
            break;
        case 'array':
            yield* subtypesInProperty(propname, proptype.items, path);
            break;
        case 'object':
            if (proptype['x-key']) {
                yield* subtypesInProperty(propname, proptype['x-key'], path);
                yield* subtypesInProperty(propname, proptype.additionalProperties, path);
            } else {
                for (let [pn, pt] of Object.entries(proptype.properties))
                    yield* subtypesInProperty(pn, pt, path.push(propname));
            }
            break;
        default: break;
    }
}


function makeTypeRef(namespace, typename) {
    return {
                "$ref": `#/components/schemas/${namespace}.${typename}`,
                "x-type": typename
           };
}
function makeTypeRefR(ref) {
    return {
        "$ref": ref,
        "x-type": ref.split('.').pop()
    };
}

function makeEnum(values) {
    return {
        "type": 'string', //type for enums describes the encoding, not the backing type for the DSL type
        "x-type": 'string',
        "enum": values,
        "description": `(Legal values:  ${values.join(', ')})`
    };
}

function addType(namespace, name, type, spec) {
    if (`${namespace}.${name}` in spec.components.schemas)
        throw `Type ${name} already exists in ${namespace}`;
    spec.components.schemas[`${namespace}.${name}`] = type;
    return `#/components/schemas/${namespace}.${name}`;
}



module.exports = {
    fixSchema
};
