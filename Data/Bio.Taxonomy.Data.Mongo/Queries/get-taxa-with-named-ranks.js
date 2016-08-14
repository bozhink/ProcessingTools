db.taxa.aggregate([{
    $unwind: "$ranks"
}, {
    $lookup: {
        from: "taxonRankType",
        localField: "ranks",
        foreignField: "rankType",
        as: "ranks"
    }
}, {
    $unwind: "$ranks"
}, {
    $project: {
        name: 1,
        isWhiteListed: 1,
        rank: "$ranks.name"
    }
}, {
    $group: {
        _id: {
            name: "$name",
            isWhiteListed: "$isWhiteListed"
        },
        ranks: {
            $addToSet: "$rank"
        }
    }
}])