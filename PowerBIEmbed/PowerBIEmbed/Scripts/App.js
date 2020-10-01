$(document).ready(function () {

    var models = window['powerbi-client'].models;

    function EmbedPowerBI(accessToken, tokenType, type, id, embedUrl, permissions) {
        // Get models. models contains enums that can be used.
        

        var embedConfiguration = {
            type: type,
            id: id,
            embedUrl: embedUrl,
            tokenType: tokenType === 0 ? models.TokenType.Aad : models.TokenType.Embed,
            accessToken: accessToken,
            permissions: permissions,
            settings: {
                filterPaneEnabled: true,
                navContentPaneEnabled: true
            }
        };

        var $reportContainer = $('#reportContainer');
        var report = powerbi.embed($reportContainer.get(0), embedConfiguration);
    }

    $.getJSON("/Home/AccessTokenPowerBI")
        .done(function (result) {
            console.log(result);
            EmbedPowerBI(result.accessToken, 0, "report", result.reportId, result.reportEmbedUrl,null);
        });

    

});
    