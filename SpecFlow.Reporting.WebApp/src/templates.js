angular.module('app').run(['$templateCache', function($templateCache) {
  'use strict';

  $templateCache.put('directives/result-badge.tpl.html',
    "<span class=\"label pull-right\" ng-class=\"{ \r" +
    "\n" +
    "    'label-success': reportItem.result == 'OK', \r" +
    "\n" +
    "    'label-danger': reportItem.result == 'Error', \r" +
    "\n" +
    "    'label-warning': reportItem.result == 'Pending', \r" +
    "\n" +
    "    'label-default': reportItem.result == 'NotRun' }\"><span ng-if=\"!reportItem.exception\">{{reportItem.result}}</span> <span ng-if=\"reportItem.exception\" ng-click=\"showException()\" tooltip=\"{{reportItem.exception.message}}\" tooltip-placement=\"left\" tooltip-append-to-body=\"true\">{{reportItem.result}}</span></span>"
  );


  $templateCache.put('templates/exception-details-modal.tpl.html',
    "<div class=\"modal-header\"><h3 class=\"modal-title\">Exception details</h3></div><div class=\"modal-body\"><dl class=\"dl-horizontal\"><dt>Message</dt><dd>{{exception.message}}</dd><dt>Type</dt><dd>{{exception.exception_type}}</dd><dt>Source</dt><dd>{{exception.source}}</dd><dt>Stack trace</dt><dd><pre>{{exception.stack_trace}}</pre></dd></dl></div><div class=\"modal-footer ng-scope\"><button class=\"btn btn-primary\" ng-click=\"ok()\">Close</button></div>"
  );


  $templateCache.put('views/dashboard.tpl.html',
    "<div ng-controller=\"DashboardController\"><nav class=\"navbar navbar-default navbar-fixed-top\" role=\"navigation\"><div class=\"container\"><!-- Brand and toggle get grouped for better mobile display --><div class=\"navbar-header\"><a class=\"navbar-brand\" href=\"#\">__TITLE__</a></div><div class=\"collapse navbar-collapse\"><form class=\"navbar-form navbar-right\" role=\"search\"><div class=\"form-group\"><input type=\"text\" class=\"form-control\" placeholder=\"Search\"></div></form></div></div></nav><div class=\"container\">__DASHBOARD_TEXT__<h1>Features</h1><accordion close-others=\"oneAtATime\"><accordion-group ng-repeat=\"feature in report.features\" is-open=\"feature.$$isOpen\"><accordion-heading>{{feature.title}} <small class=\"text-muted\"><em>completed in {{feature.duration}} ms</em></small><result-badge report-item=\"feature\"></result-badge></accordion-heading><div ng-bind-html=\"feature.description_html | trusted\"></div><h3>Scenarios</h3><div class=\"list-group\"><a ng-repeat=\"scenario in feature.scenarios\" class=\"list-group-item\" href=\"#/features/{{feature.id|encodeURIComponent}}/scenarios/{{scenario.id|encodeURIComponent}}\">{{scenario.title}} <small class=\"text-muted\"><em>completed in {{scenario.duration}} ms</em></small><result-badge report-item=\"scenario\"></result-badge></a></div></accordion-group></accordion></div></div>"
  );


  $templateCache.put('views/scenario.tpl.html',
    "<div ng-controller=\"ScenarioController\"><nav class=\"navbar navbar-default navbar-fixed-top\" role=\"navigation\"><div class=\"container-fluid\"><!-- Collect the nav links, forms, and other content for toggling --><div class=\"navbar-left\"><div class=\"navbar-left\"><a href=\"#/dashboard/feature/{{feature.id}}\" role=\"button\" class=\"btn btn-default navbar-btn\"><span class=\"glyphicon glyphicon-chevron-left\"></span>Back</a></div></div><div class=\"collapse navbar-collapse text-center\"><p class=\"navbar-text\">{{feature.title}}</p></div></div></nav><div class=\"container\"><h4>Scenario: {{scenario.title}}</h4><div class=\"panel panel-default\"><div class=\"panel-heading\"><h3 class=\"panel-title\">Given</h3></div><div class=\"panel-body\"><ul class=\"list-group\"><li class=\"list-group-item step-details\" ng-include=\"'step-details'\" ng-repeat=\"step in scenario.given.steps\"></li></ul></div></div><div class=\"panel panel-default\"><div class=\"panel-heading\"><h3 class=\"panel-title\">When</h3></div><div class=\"panel-body\"><ul class=\"list-group\"><li class=\"list-group-item step-details\" ng-include=\"'step-details'\" ng-repeat=\"step in scenario.when.steps\"></li></ul></div></div><div class=\"panel panel-default\"><div class=\"panel-heading\"><h3 class=\"panel-title\">Then</h3></div><div class=\"panel-body\"><ul class=\"list-group\"><li class=\"list-group-item step-details\" ng-include=\"'step-details'\" ng-repeat=\"step in scenario.then.steps\"></li></ul></div></div></div><script id=\"step-details\" type=\"text/ng-template\"><!-- step-details-marker: begin -->\r" +
    "\n" +
    "\t\t{{step.title}} <small class=\"text-muted\"><em>completed in {{step.duration}} ms</em></small>\r" +
    "\n" +
    "        <result-badge report-item=\"step\"></result-badge>\r" +
    "\n" +
    "\r" +
    "\n" +
    "\t\t<!-- step-details-marker: end --></script></div>"
  );

}]);
