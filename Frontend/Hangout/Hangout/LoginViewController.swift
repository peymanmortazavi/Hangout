//
//  LoginViewController.swift
//  Hangout
//
//  Created by Vahid Mazdeh on 11/7/14.
//  Copyright (c) 2014 Peyman Mortazavi. All rights reserved.
//

import Foundation
import UIKit


class LoginViewController: UIViewController {
    @IBOutlet weak var firstNameLabel: UILabel!
    
    override func viewDidLoad() {
        super.viewDidLoad()
        
        //Comment these if you want to test with no connection
        var client: HangoutClient = HangoutClient(apiUrl: "http://10.202.21.182:8080/")
        firstNameLabel.text = client.testGet()
        
    }
    
    override func didReceiveMemoryWarning() {
        super.didReceiveMemoryWarning()
        // Dispose of any resources that can be recreated.
    }
    
    
}
