(function() {
    'use strict';

    angular.module('app').factory('Report', [
        function($scope, $routeParams) {
            function Report(reportData) {
                this.reportData = reportData;

                // Create a grouped property for each report item type
                this.features = reportData.features;
                this.scenarios = _.flatten(reportData.features, 'scenarios');
                this.scenarioBlocks = _.reduce(this.scenarios, function(result, scenario) {
                    result.push(scenario.given);
                    result.push(scenario.when);
                    result.push(scenario.then);
                    return result;
                }, []);
                this.steps = _.reduce(this.scenarioBlocks, function(result, scenarioBlock) {
                    result.push.apply(result, scenarioBlock.steps);
                    return result;
                }, []);

                // Decorate features
                _.forEach(this.reportData.features, function (feature, index) {
                    feature.id = generateId([feature.title]);
                    feature.duration = getDuration(feature);

                    // Decorate scenarios
                    _.forEach(feature.scenarios, function(scenario, index) {
                        scenario.id = generateId([feature.title, scenario.title]);
                        scenario.duration = getDuration(scenario);
                        scenario.feature = feature;

                        var stepNumber = 1;
                        
                        // Decorate scenario blocks
                        var scenarioblockMap = _.pick(scenario, ['given', 'when', 'then']);
                        _.forEach(scenarioblockMap, function(scenarioBlock, scenarioBlockType) {
                            scenarioBlock.id = generateId([feature.title, scenario.title, scenarioBlockType]);
                            scenarioBlock.duration = getDuration(scenarioBlock);
                            scenarioBlock.feature = feature;
                            scenarioBlock.scenario = scenario;

                            // Decorate steps
                            _.forEach(scenarioBlock.steps, function(step, index) {
                                step.id = generateId([feature.title, scenario.title, scenarioBlockType, index]);
                                step.duration = getDuration(step);
                                step.feature = feature;
                                step.scenario = scenario;
                                step.scenarioBlock = scenarioBlock;
                                step.number = stepNumber++;

                                // Store step's exception on the scenario and scenarioblock.
                                // Assume there can be only one exception in scenario
                                if (step.exception) {
                                    scenario.exception = step.exception;
                                    scenarioBlock.exception = step.exception;
                                }
                            });
                        });
                    });
                });
            }

            function getDuration(item) {
                return new Date(item.end_time).getTime() - new Date(item.start_time).getTime();
            }

            function generateId(values) {
                var str = values.join('#####');
                return md5(str);
            };

            Report.prototype = {
                constructor: Report,

                findFeatureById: function (id) {
                    return _.find(this.features, function (feature) {
                        return feature.id === id;
                    });
                },

                findScenarioById: function (id) {
                    return _.find(this.scenarios, function (scenario) {
                        return scenario.id === id;
                    });
                },

                findStepById: function (id) {
                    return _.find(this.steps, function (step) {
                        return scenario.id === id;
                    });
                }
            };

            

            return Report;
        }
    ]);
})();