/* globals db */

db.getCollection('taxa').aggregate([

    {$project: {'_id': 0}},

    {$unwind: '$ranks'},

    {$group: {
        '_id': {
            'name': '$name',

            'isWhiteListed': '$isWhiteListed'
        },

        'numberOfRanks': {$sum: 1}
    }},

    {$match: { 'numberOfRanks': { $gt: 1 } }},

    {$project: { '_id': '$_id.name', 'numberOfRanks': '$numberOfRanks'}}

    // /*Uncomment to view the number of different items*/,{$group: {_id: 1, n: {$sum: 1}}}

]);
