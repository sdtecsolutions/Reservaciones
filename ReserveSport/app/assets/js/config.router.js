'use strict';

/**
 * Config for the router
 */

app.config(['$stateProvider', '$urlRouterProvider', '$controllerProvider', '$compileProvider', '$filterProvider', '$provide', '$ocLazyLoadProvider', 'JS_REQUIRES',
function ($stateProvider, $urlRouterProvider, $controllerProvider, $compileProvider, $filterProvider, $provide, $ocLazyLoadProvider, jsRequires) {

    app.controller = $controllerProvider.register;
    app.directive = $compileProvider.directive;
    app.filter = $filterProvider.register;
    app.factory = $provide.factory;
    app.service = $provide.service;
    app.constant = $provide.constant;
    app.value = $provide.value;

    // LAZY MODULES

    $ocLazyLoadProvider.config({
        debug: false,
        events: true,
        modules: jsRequires.modules
    });

    // APPLICATION ROUTES
    // -----------------------------------
    // For any unmatched url, redirect to /app/dashboard
    $urlRouterProvider.otherwise("/app/reservation");
    //
    // Set up the states
    $stateProvider.state('app', {
        url: "/app",
        templateUrl: "assets/views/app.html",
        resolve: loadSequence('modernizr', 'moment', 'angularMoment', 'uiSwitch', 'perfect-scrollbar-plugin', 'toaster', 'ngAside', 'vAccordion', 'sweet-alert', 'chartjs', 'tc.chartjs', 'oitozero.ngSweetAlert', 'chatCtrl', 'truncate', 'htmlToPlaintext', 'angular-notification-icons'),
        abstract: true
    }).state('app.reservation', {
        url: "/reservation",
        templateUrl: "assets/views/reservation.html",
        resolve: loadSequence('reservationCtrl'),
        title: 'Reservation',
        ncyBreadcrumb: {
            label: 'Reservation'
        }
    }).state('app.ui', {
        url: '/ui',
        template: '<div ui-view class="fade-in-up"></div>',
        title: 'UI Reservation',
        ncyBreadcrumb: {
            label: 'UI Reservation'
        }
    }).state('app.ui.typesport', {
        url: '/typesport',
        templateUrl: "assets/views/typessport.html",
        title: 'Types of Sport',
        icon: 'ti-layout-media-left-alt',
        resolve: loadSequence('typessportCtrl'),
        ncyBreadcrumb: {
            label: 'typesport'
        }
    }).state('app.ui.typecourts', {
        url: '/typecourts',
        templateUrl: "assets/views/typescourts.html",
        title: 'Types of Courts',
        icon: 'ti-layout-media-left-alt',
        resolve: loadSequence('typescourtsCtrl'),
        ncyBreadcrumb: {
            label: 'typecourts'
        }
    }).state('app.ui.timetable', {
        url: '/timetable',
        templateUrl: "assets/views/timetable.html",
        title: 'TimeTable',
        resolve: loadSequence('timetableCtrl'),
        ncyBreadcrumb: {
            label: 'TimeTable'
        }
    }).state('app.ui.report1', {
        url: '/report1',
        templateUrl: "assets/views/report1.html",
        title: 'Reporte 1',
        resolve: loadSequence('report1Ctrl'),
        ncyBreadcrumb: {
            label: 'Reporte 1'
        }
    })

	// Login routes

	.state('login', {
	    url: '/login',
	    template: '<div ui-view class="fade-in-right-big smooth"></div>',
	    abstract: true
	}).state('login.signin', {
	    url: '/signin',
	    templateUrl: "assets/views/login_login.html",
	    resolve: loadSequence('loginCtrl')
	}).state('login.forgot', {
	    url: '/forgot',
	    templateUrl: "assets/views/login_forgot.html"
	}).state('login.registration', {
	    url: '/registration',
	    templateUrl: "assets/views/login_registration.html"
	}).state('login.lockscreen', {
	    url: '/lock',
	    templateUrl: "assets/views/login_lock_screen.html"
	})

    // Solicitudes routes
    .state('orders', {
        url: '/orders',
        template: '<div ui-view class="fade-in-right-big smooth"></div>',
        abstract: true
    }).state('orders.order', {
        url: '/order',
        templateUrl: "assets/views/order.html",
        resolve: loadSequence('orderCtrl')
    });

    // Generates a resolve object previously configured in constant.JS_REQUIRES (config.constant.js)
    function loadSequence() {
        var _args = arguments;
        return {
            deps: ['$ocLazyLoad', '$q',
			function ($ocLL, $q) {
			    var promise = $q.when(1);
			    for (var i = 0, len = _args.length; i < len; i++) {
			        promise = promiseThen(_args[i]);
			    }
			    return promise;

			    function promiseThen(_arg) {
			        if (typeof _arg == 'function')
			            return promise.then(_arg);
			        else
			            return promise.then(function () {
			                var nowLoad = requiredData(_arg);
			                if (!nowLoad)
			                    return $.error('Route resolve: Bad resource name [' + _arg + ']');
			                return $ocLL.load(nowLoad);
			            });
			    }

			    function requiredData(name) {
			        if (jsRequires.modules)
			            for (var m in jsRequires.modules)
			                if (jsRequires.modules[m].name && jsRequires.modules[m].name === name)
			                    return jsRequires.modules[m];
			        return jsRequires.scripts && jsRequires.scripts[name];
			    }
			}]
        };
    }
}]);