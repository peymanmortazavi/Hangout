//
//  ViewController.swift
//  Hangout
//
//  Created by Peyman Mortazavi on 11/6/14.
//  Copyright (c) 2014 Peyman Mortazavi. All rights reserved.
//

import UIKit

class ViewController: UIViewController {

    override func viewDidLoad() {
        super.viewDidLoad()
        
        //Comment these if you want to test with no connection
        var connection: SRConnection = SRConnection(URL: "http://172.29.129.140:8080/test");
        connection.start()

    }

    override func didReceiveMemoryWarning() {
        super.didReceiveMemoryWarning()
        // Dispose of any resources that can be recreated.
    }


}

