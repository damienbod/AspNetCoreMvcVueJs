const HtmlWebpackPlugin = require('html-webpack-plugin');

console.log('@@@@@@@@@ VUE.JS Build @@@@@@@@@@@@@@@');

module.exports = {
    outputDir: '../wwwroot',
    filenameHashing: true,
    configureWebpack: {

        plugins: [
            new HtmlWebpackPlugin({
                filename: '../../Views/Shared/_Layout.cshtml',
                inject: 'body',
                template: 'public/_Layout.cshtml'
            })
        ]
    }
}