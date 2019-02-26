const webpack = require('webpack');
const path = require('path');

/*
 * SplitChunksPlugin is enabled by default and replaced
 * deprecated CommonsChunkPlugin. It automatically identifies modules which
 * should be splitted of chunk by heuristics using module duplication count and
 * module category (i. e. node_modules). And splits the chunks…
 *
 * It is safe to remove "splitChunks" from the generated configuration
 * and was added as an educational example.
 *
 * https://webpack.js.org/plugins/split-chunks-plugin/
 *
 */

/*
 * We've enabled UglifyJSPlugin for you! This minifies your app
 * in order to load faster and run less javascript.
 *
 * https://github.com/webpack-contrib/uglifyjs-webpack-plugin
 *
 */

const UglifyJSPlugin = require('uglifyjs-webpack-plugin');

module.exports = {
    entry: ["@babel/polyfill", './index.js'],
    module: {        
		rules: [
			{
                loader: 'babel-loader',
                exclude: /node_modules/,

				options: {
					plugins: ['syntax-dynamic-import'],

					presets: [
						[
							'@babel/preset-env',
							{
								modules: false
							}
						]
					]
				},

				test: /\.js$/
            },

            {
                loader: 'babel-loader',
                exclude: /node_modules/,

                options: {
                    plugins: ['syntax-dynamic-import'],
                    presets:["@babel/preset-env", "@babel/preset-typescript"]
                },

                test: /\.ts$/
            }
        ],
        
    },
    resolve: {
        modules: [
            "node_modules",
            path.resolve(__dirname)
        ],
        extensions: [".js"]
    },

	output: {
		filename: 'bundle.js',
		path: path.resolve(__dirname, 'dist')
    },

    node: {
        console: false,
        request: 'empty'
    },

    mode: 'production'
};
