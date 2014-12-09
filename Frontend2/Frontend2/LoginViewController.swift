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
    //@IBOutlet weak var firstNameLabel: UILabel!
    
    @IBOutlet weak var emailField: UITextField!
    @IBOutlet weak var passwordField: UITextField!
    
    override func viewDidLoad() {
        super.viewDidLoad()
        
        // Comment these out (and more in ViewController) to test with no server connection
        //var client: HangoutClient = HangoutClient(apiUrl: "http://10.202.21.182:8080/")
        //firstNameLabel.text = client.testGet()
        
    }
    
    @IBAction func AttemptToLogin(sender: UIButton) {
        let url = NSURL(string: HangoutClient.ApiUrl + "accounts/authenticate")!
        var request: NSMutableURLRequest = NSMutableURLRequest(URL: url)
        request.HTTPMethod = "POST"
        request.addValue("application/json", forHTTPHeaderField: "Content-Type")
        let loginDictionary = ["email": emailField.text!, "password": passwordField.text!]
        let data = NSJSONSerialization.dataWithJSONObject(loginDictionary, options: NSJSONWritingOptions(0), error: nil)
        request.HTTPBody = data
        var response: AutoreleasingUnsafeMutablePointer <NSURLResponse?> = nil
        var error: AutoreleasingUnsafeMutablePointer<NSError?> = nil
        
        var dataVal: NSData? = NSURLConnection.sendSynchronousRequest(request, returningResponse: response, error:error)
        
        if let data = dataVal{
            let json = NSJSONSerialization.JSONObjectWithData(data, options: NSJSONReadingOptions.MutableContainers, error: nil) as NSDictionary
            println(json)
            let firstNameOpt = json["firstName"] as NSString?
            if let firstName = firstNameOpt{
                //We have a name!
                self.performSegueWithIdentifier("LoginToFriends", sender: self)
            }else{
                //We have an error!
            }
            //Either use UIAlertView to show error, or call self.presentViewController(the other view controller)
        }else{
            //Use UIAlertView to show error
        }
        
        // Tests if request was successful
        /*if let data = dataVal {
            var err: NSError? = nil
            var jsonResult: NSDictionary = NSJSONSerialization.JSONObjectWithData(data, options: NSJSONReadingOptions.MutableContainers, error: &err) as NSDictionary
            //return true;
        }*/
        
        //return false;
        
        // Format into JSON
        // Call Login(LoginModel) with an HTTP POST
        // If login was successful, go to Friends
        // Else do nothing, or clear email and password
    }
    
    override func didReceiveMemoryWarning() {
        super.didReceiveMemoryWarning()
        // Dispose of any resources that can be recreated.
    }
    
    
}
