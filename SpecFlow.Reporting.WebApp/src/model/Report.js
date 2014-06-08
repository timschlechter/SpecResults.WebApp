(function() {
    'use strict';

    angular.module('app').factory('Report', [
        function($scope, $routeParams) {
            function Report(reportData) {
                this.reportData = reportData;

                this.features = reportData.features;
                this.scenarios = _.flatten(reportData.features, 'scenarios');
                this.steps = _.reduce(this.scenarios, function (result, scenario) {
                    result.push.apply(result, scenario.given.steps);
                    result.push.apply(result, scenario.when.steps);
                    result.push.apply(result, scenario.then.steps);
                    return result;
                }, []);

                _.forEach(this.reportData.features, function (feature, index) {
                    feature.id = generateId([feature.title]);
                    feature.duration = getDuration(feature);

                    _.forEach(feature.scenarios, function(scenario, index) {
                        scenario.id = generateId([feature.title, scenario.title]);
                        scenario.duration = getDuration(scenario);

                        scenario.given.duration = getDuration(scenario.given);
                        _.forEach(scenario.given.steps, function(step, index) {
                            step.id = generateId([feature.title, scenario.title, 'given', index]);
                            step.duration = getDuration(step);
                        });

                        scenario.when.duration = getDuration(scenario.when);
                        _.forEach(scenario.when.steps, function(step, index) {
                            step.id = generateId([feature.title, scenario.title, 'when', index]);
                            step.duration = getDuration(step);
                        });

                        scenario.then.duration = getDuration(scenario.then);
                        _.forEach(scenario.then.steps, function(step, index) {
                            step.id = generateId([feature.title, scenario.title, 'then', index]);
                            step.duration = getDuration(step);
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