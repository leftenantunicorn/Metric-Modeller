#########################################################
## Make prediction for single record

try:
    import sys
    import numpy as np
    import pandas as pd
    from decimal import Decimal
    from sklearn.preprocessing import Imputer
    from sklearn.multioutput import MultiOutputRegressor
    from sklearn.ensemble import RandomForestRegressor
    from sklearn.cross_validation import train_test_split
    import simplejson as json

    df = pd.read_csv(sys.argv[1], encoding='utf-8-sig')

    x_single = [[Decimal(n) for n in sys.argv[2].split(",")]];

    # Prepare feature and result sets
    feature_col_names = ['prec', 'flex', 'resl', 'team', 'pmat', 'rel', 'data', 'cplx', 'ruse', 'docu', 'time', 'stor', 'pvol', 'acap', 'pcap', 'pcon', 'apex', 'plex', 'ltex', 'tool', 'site', 'sched']
    predicted_class_names = ['kloc', 'effort', 'defects','months','cost']

    x = df[feature_col_names].values
    y = df[predicted_class_names].values
    split_test_size = 0.30

    
    x_train, x_test, y_train, y_test = train_test_split(x, y,
                                                    train_size=split_test_size,
                                                    random_state=4)

    # Manipulate bad data
    fill_0 = Imputer(missing_values=0, strategy="mean", axis=0)
    x_train = fill_0.fit_transform(x_train)

    # Train model
    max_depth = 30
    rf = RandomForestRegressor(max_depth=max_depth,random_state=0)
    regr_multirf = MultiOutputRegressor(rf)
    regr_multirf.fit(x_train, y_train)

    print(json.dumps({
        "kloc" : regr_multirf.predict(x_single)[0][0], 
        "effort" : regr_multirf.predict(x_single)[0][1], 
        "defects" : regr_multirf.predict(x_single)[0][2], 
        "months" : regr_multirf.predict(x_single)[0][3], 
        "cost" : regr_multirf.predict(x_single)[0][4]
        }), end="")

except Exception as e:
    print ("Unexpected error:", format(e) )