import re
cvs = ["1abc2",
    "pqr3stu8vwx",
    "a1b2c3d4e5f"
    "treb7uchet"]

fcvs = []
for cv in cvs:
    ds = []
    print ("word: " + cv)
    for j, c in enumerate(cv):
        if c.isdigit():
            print ("digit value: " + c)
            ds.append(c)
    cds = ds[0] + ds[-1]
    print ("thingys: " + cds)
    fcvs.append(cds)

print (fcvs)